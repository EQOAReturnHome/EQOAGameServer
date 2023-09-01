function  event_say(choice)
diagOptions = {}
    npcDialogue = "I specialize in creating leather bindings for new books. Let me know if you need some."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end