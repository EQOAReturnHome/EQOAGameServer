function  event_say(choice)
diagOptions = {}
    npcDialogue = "Thanks for visitin' Bobble-by-Water, playerName. Be sure to enjoy yerself, mind yer manners, and we deputies will leave you be."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end