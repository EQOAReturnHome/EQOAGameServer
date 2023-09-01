function  event_say(choice)
diagOptions = {}
    npcDialogue = "There was a dwarf here not took long ago, went by the name of Xylof. Strange, that one. Seem shook by something. Left without saying a word."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end