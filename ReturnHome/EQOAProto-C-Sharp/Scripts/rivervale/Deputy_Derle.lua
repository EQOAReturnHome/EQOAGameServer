function  event_say(choice)
diagOptions = {}
    npcDialogue = "This is the main entrance to Rivervale. Once inside, you can relax. We'll keep you safe."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end