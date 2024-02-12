using System;

public interface IInteractableInterface
{
    string InteractionPrompt { get; }
    bool Interact(Interactor interactor);
}