using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemVisual : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Item currentItem;
    public Item Item
    {
        get
        {
            return currentItem;
        }
        set
        {
            currentItem = value;
            Image.sprite = currentItem.Sprite;
        }
    }
    [SerializeField] private Image Image;
    private CanvasGroup canvasGroup;
    private Transform initialParent;

    public IItemSlot Slot;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || Item == null)
            return;

        initialParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        transform.SetParent(transform.parent.parent.parent.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || Item == null)
            return;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        transform.SetParent(initialParent);
        transform.localPosition = Vector2.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item != null)
            Tooltip.Instance.Show(Item.TooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.Hide();
    }

    public void SetParent(Transform newParent, IItemSlot newSlot)
    {
        initialParent = newParent;
        transform.SetParent(newParent);
        transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        transform.localPosition = Vector2.zero;
        Slot = newSlot;
    }
}
