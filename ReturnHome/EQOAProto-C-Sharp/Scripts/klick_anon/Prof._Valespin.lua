function  event_say(choice)
diagOptions = {}
    npcDialogue = "With a proper staff, you may be able to stream the power of magic to others, in their time of need."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end