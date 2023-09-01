function  event_say(choice)
diagOptions = {}
    npcDialogue = "Out of my way, fool.  I have things to do, and I can't have a simple minded spit like you distracting me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end