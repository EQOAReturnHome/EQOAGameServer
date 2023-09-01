function  event_say(choice)
diagOptions = {}
    npcDialogue = "Be a good sport and fetch me an ale, will ya playerName? The bar is just behind this wall."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end