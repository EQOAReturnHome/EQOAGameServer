function  event_say(choice)
diagOptions = {}
    npcDialogue = "Why don't you stay here for a spell, playerName. I'll bring you a cocktail, a favorite of Klick'Anon!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end