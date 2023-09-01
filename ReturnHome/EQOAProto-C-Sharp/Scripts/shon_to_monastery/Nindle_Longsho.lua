function event_say()
diagOptions = {}
    npcDialogue = "Venathorn drives me nuts! He knows darn well that the red burn berries are native to Fayspire but he'd rather send you to me to purchase them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end