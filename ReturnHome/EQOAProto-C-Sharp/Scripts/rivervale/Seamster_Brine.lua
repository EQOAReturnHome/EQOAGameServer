function  event_say(choice)
diagOptions = {}
    npcDialogue = "The art of tailoring is one of sophistication and delicacy. Your eyes must be keen and your hands steady. Shall we begin?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end