function  event_say(choice)
diagOptions = {}
    npcDialogue = "I detest having to patrol anywhere near the druid guild on a rainy day. It always smells like wet dogs."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end