function event_say()
diagOptions = {}
    npcDialogue = "I don't know if we will ever make it across the Ocean of Tears to Faydewer. I think our chances of survival are with the new spires which are being built across the lake. Perhaps I should go and offer my assistance."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end