using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    public enum State {
        Idle, 
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (HasKitchenItem()) {
            switch (state) {
                case State.Idle:
                    break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimeMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimeMax) {
                        GetKitchenItem().DestroySelf();
                        KitchenItem.SpawnKitchenItem(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = this.state
                        });
                    }
                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimeMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimeMax) {
                        GetKitchenItem().DestroySelf();
                        KitchenItem.SpawnKitchenItem(burningRecipeSO.output, this);
                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = this.state
                        });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = 0f
                    });

                    }
                    break;

                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenItem()) {
            // There is no KitchenItem here
            if (player.HasKitchenItem()) {
                // PLayer carrying KitchenItem
                if (HasRecipeWithInput(player.GetKitchenItem().GetKitchenItemSO())) {
                    player.GetKitchenItem().SetKitchenItemParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());
                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                        state = this.state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimeMax
                    });

                }
            } else {
                // Player has nothing
            }
        } else {
            // There is no KitchenItem here
            if (player.HasKitchenItem()) {
                // Player is carrying something
                if (player.GetKitchenItem().TryGetPlate(out PlateKitchenItem plateKitchenItem)) {
                    // player is holding a plate
                    if (plateKitchenItem.TryAddIngredient(GetKitchenItem().GetKitchenItemSO())) {
                        GetKitchenItem().DestroySelf();
                        
                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = this.state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });  
                    }
                }
            } else {
                // PLayer is not carrying anything
                GetKitchenItem().SetKitchenItemParent(player);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = this.state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });                
            }

        }
        
    }

    private bool HasRecipeWithInput(KitchenItemSO inputKitchenItemSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenItemSO);
        return fryingRecipeSO != null;
    }

    private KitchenItemSO GetOutputForInput(KitchenItemSO inputKitchenItemSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenItemSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        } else {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenItemSO inputKitchenItemSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenItemSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }
     
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenItemSO inputKitchenItemSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == inputKitchenItemSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
