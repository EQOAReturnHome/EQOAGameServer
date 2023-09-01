function  event_say(choice)
diagOptions = {}
    npcDialogue = "The snakes in the Unkempt Glade have taken over my home! I came here hoping the wood elves could help me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end