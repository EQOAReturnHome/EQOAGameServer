function  event_say(choice)
diagOptions = {}
    npcDialogue = "Keep your eyes peeled.  Some shifty characters occupy this city.  Keep your pocket tight, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end