function  event_say(choice)
diagOptions = {}
    npcDialogue = "Binding spirits to this place is my task. It isn't a pretty job but at least it is honest and keeps the three families away."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end