function  event_say(choice)
diagOptions = {}
    npcDialogue = "That Nanie poisoned my pie, I just know it. She's such a wretch!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end