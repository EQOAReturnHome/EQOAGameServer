function  event_say(choice)
diagOptions = {}
    npcDialogue = "Behave yourself here.  I won't hesitate to slay you if need be."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end