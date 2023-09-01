function  event_say(choice)
diagOptions = {}
    npcDialogue = "A ranger is at one with nature, our abilities in archery and woodslore are second to none. Our primary goal is to guard nature."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end