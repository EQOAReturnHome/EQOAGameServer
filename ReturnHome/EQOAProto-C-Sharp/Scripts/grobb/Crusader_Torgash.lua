function  event_say(choice)
diagOptions = {}
    npcDialogue = "The Frogloks must be squashed before they become too many. They multiply too quickly. We must find their weakness and exploit it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end