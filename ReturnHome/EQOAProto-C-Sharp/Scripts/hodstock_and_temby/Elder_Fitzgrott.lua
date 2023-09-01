function  event_say(choice)
diagOptions = {}
    npcDialogue = "We don't take kindly to strangers in this town. Be sure you move along soon drifter."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end