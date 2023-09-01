function event_say()
diagOptions = {}
    npcDialogue = "Welcome to the Bank of Highbourne. Be ready to complete your transactions swiftly and continue on your business."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end