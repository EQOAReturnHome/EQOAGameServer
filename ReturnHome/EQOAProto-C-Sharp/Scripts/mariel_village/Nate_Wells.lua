function  event_say(choice)
diagOptions = {}
    npcDialogue = "By the grace of The Great Mother Tunare, we will honor and protect all nature here in Mariel Village. You may also be loved and blessed by Tunare, no matter what you may have done in this lifetime."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end