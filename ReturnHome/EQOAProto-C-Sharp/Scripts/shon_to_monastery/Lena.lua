function event_say()
diagOptions = {}
    npcDialogue = "I have learned much from Kaiaren, though I do pity him. He often mumbles to himself about a key, but he won't speak of it when I mention it. You can see torment in his eyes."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end