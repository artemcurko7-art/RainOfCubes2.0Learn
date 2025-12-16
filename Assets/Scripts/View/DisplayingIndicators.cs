using TMPro;
using UnityEngine;

public abstract class DisplayingIndicators : MonoBehaviour
{
    [SerializeField] private TMP_Text _viewSpawnedText;
    [SerializeField] private TMP_Text _viewCreatedText;
    [SerializeField] private TMP_Text _viewActivatedText;

    protected void View(int amountSpawnedObjects, int amountCreatedObjects, int amountActivedObjects)
    {
        _viewSpawnedText.text = amountSpawnedObjects.ToString();
        _viewCreatedText.text = amountCreatedObjects.ToString();
        _viewActivatedText.text = amountActivedObjects.ToString();
    }
}
