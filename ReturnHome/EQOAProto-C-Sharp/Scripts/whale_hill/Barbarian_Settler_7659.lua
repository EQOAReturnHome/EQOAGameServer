function  event_say(choice)
diagOptions = {}
    npcDialogue = "We help the city of Qeynos by delivering supplies to and from the villages to the north."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end