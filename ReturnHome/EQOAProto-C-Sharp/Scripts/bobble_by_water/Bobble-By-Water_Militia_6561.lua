function  event_say(choice)
diagOptions = {}
    npcDialogue = "The dark elves are thieves and murderers, pure and simple. They wouldn't hesitate to wipe out this town if they had the chance."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end