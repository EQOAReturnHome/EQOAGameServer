function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can send you to Klick'Anon but it doesn't look like you have the appropriate approval. Best take it up with the Ghobber family. They control everything that comes in and out of Rivervale."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end