function  event_say(choice)
diagOptions = {}
    npcDialogue = "Would you perhaps be willing to help me feed these patrons? A few fish, for some Tunar?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end