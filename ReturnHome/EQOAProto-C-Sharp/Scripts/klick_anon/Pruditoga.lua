function  event_say(choice)
diagOptions = {}
    npcDialogue = "Excuse me, I am waiting for someone. Someone powerful and brave. At any moment, they should be returning from an epic journey."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end