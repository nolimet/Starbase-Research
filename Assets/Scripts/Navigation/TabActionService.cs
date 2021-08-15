using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TabActionService : ITickable
{
    private EventSystem eventsystem;

    public void Tick()
    {
        if (!eventsystem)
        {
            eventsystem = EventSystem.current;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && eventsystem.currentSelectedGameObject)
        {
            var nextAction = eventsystem.currentSelectedGameObject.GetComponent<TabNavigationAction>();

            if (nextAction.Next)
            {
                eventsystem.SetSelectedGameObject(nextAction.Next.gameObject);
            }
        }
    }
}