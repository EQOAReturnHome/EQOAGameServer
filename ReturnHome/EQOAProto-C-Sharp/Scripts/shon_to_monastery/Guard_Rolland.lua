function event_say()
diagOptions = {}
    npcDialogue = "Best decision of my life to come to Chiasta. The work is honest and the pay decent…though I am a bit tired of the bean sprouts the monks like to cook for meals."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end