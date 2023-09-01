function  event_say(choice)
diagOptions = {}
    npcDialogue = "Mining is in a dwarfs blood. Ma great-great grandfather mined and built the halls o' Kaladim on the far away land o' Faydewer. Perhaps we do it for the love o' the earth. Perhaps we're tryin' ta get closer ta the Duke of Below himself..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end