function  event_say(choice)
diagOptions = {}
    npcDialogue = "Swords during the day, ale and stories at night. This is the life. Even that troll can tell a good story or two."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end