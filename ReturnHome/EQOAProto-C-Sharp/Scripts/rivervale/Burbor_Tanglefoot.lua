function  event_say(choice)
diagOptions = {}
    npcDialogue = "Karana The Rainkeeper is always watching. Be kind to all and the storms may ever be in your favor. New business won't be discussed until the day of Ruba's wedding."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end