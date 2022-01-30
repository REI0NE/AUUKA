using System.Collections.Generic;

public class InteractionList
{
    private List<IInteractionObject> _interactions = new List<IInteractionObject>(101);
    public List<IInteractionObject> Interactions => _interactions;
}
