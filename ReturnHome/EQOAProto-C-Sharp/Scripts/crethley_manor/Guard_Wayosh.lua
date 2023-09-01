function  event_say(choice)
diagOptions = {}
    npcDialogue = "I never thought I would live to see this, but a zombie has been roaming the area. This has been a bit terrifying for us to deal with."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end