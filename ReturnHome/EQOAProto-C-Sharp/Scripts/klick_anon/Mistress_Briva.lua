function  event_say(choice)
diagOptions = {}
    npcDialogue = "Dedicated to the spread of disease and deadly poison, we rogues have developed some of the most potent venoms on Tunaria. Best watch your back."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end