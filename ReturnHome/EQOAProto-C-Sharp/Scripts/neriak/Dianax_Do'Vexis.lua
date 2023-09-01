function  event_say(choice)
diagOptions = {}
    npcDialogue = "Though I'm sure you have something important to say, I cannot be bothered to listen. Get out."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end