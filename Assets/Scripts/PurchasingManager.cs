using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class PurchasingManager : Singleton<PurchasingManager>, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    private string removeAdsId = "remove_ads";

    public Button removeADS;

    public bool ADSremoved = false;

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason: " + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogFormat("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (string.Equals(purchaseEvent.purchasedProduct.definition.id, removeAdsId, System.StringComparison.Ordinal))
        {
            Debug.LogFormat("ProcessPurchase: PASS. Product : {0}", purchaseEvent.purchasedProduct.definition.id);
            PlayerPrefs.SetInt("AdsRemoved", 1);
            ADSremoved = true;
        }
        else
        {
            Debug.LogFormat("ProcessPurchase : FAIL. Unrecognized product : {0}", purchaseEvent.purchasedProduct.definition.id);
        }

        return PurchaseProcessingResult.Complete;
    }

    private void Start()
    {
        InitializePurchasing();

        removeADS.onClick.AddListener(delegate { BuyRemoveAds(); });
    }

    private void Update()
    {
        if(ADSremoved == true)
        {
            removeADS.onClick.RemoveListener(delegate { BuyRemoveAds(); });
        }
    }

    private bool IsInitialized()
    {
        return storeController != null && extensionProvider != null;
    }

    private void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(removeAdsId, ProductType.NonConsumable);
    }

    private void PurchaseProduct(string id)
    {
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(id);

            if (product != null && product.availableToPurchase)
            {
                Debug.LogFormat("Purchasing product asynchronously: {0}", product.definition.id);
                storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("PurchaseProduct FAIL. Product not found or not available for purchase.");
            }
        }
        else
        {
            Debug.Log("PurchaseProduct FAIL. Not initialized.");
        }
    }

    public void BuyRemoveAds()
    {
        PurchaseProduct(removeAdsId);
    }
}
