function  event_say(choice)
diagOptions = {}
    npcDialogue = "No, I don't know anything about the floating tower. Do I look like someone who would know about the floating tower? What floating tower? Stop pestering me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end