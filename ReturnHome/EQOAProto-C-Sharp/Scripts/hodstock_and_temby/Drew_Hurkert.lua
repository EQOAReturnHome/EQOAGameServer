function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm glad to have my daughter back after all these years. I get concerned though, she's always reading some very strange books. I wonder what thoughts they are putting into her head�"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end