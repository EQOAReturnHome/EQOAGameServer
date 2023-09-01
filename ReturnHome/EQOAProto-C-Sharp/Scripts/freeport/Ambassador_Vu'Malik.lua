function  event_say(choice)
diagOptions = {}
    npcDialogue = "Unless you intend to murder me here and now I suggest you back away. Someone is out for my blood. I can feel it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end