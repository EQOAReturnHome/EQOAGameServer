function event_say()
diagOptions = {}
    npcDialogue = "I get tired of guarding this quiet bank. If only they would allows us to read while no one is within the bank. I'm falling behind on my studies."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end