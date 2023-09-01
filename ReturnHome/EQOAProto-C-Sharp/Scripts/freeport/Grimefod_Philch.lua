function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm tending to my own business, and I would expect you to do the same! Now clear the way, I am waiting for someone."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end