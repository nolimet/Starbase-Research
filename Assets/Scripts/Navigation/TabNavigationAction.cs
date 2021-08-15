using UnityEngine;

public class TabNavigationAction : MonoBehaviour
{
    [SerializeField]
    private TabNavigationAction next;

    public TabNavigationAction Next => next;
}