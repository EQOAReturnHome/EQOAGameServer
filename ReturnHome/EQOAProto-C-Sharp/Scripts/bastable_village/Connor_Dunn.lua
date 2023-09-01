function event_say()
diagOptions = {}
    npcDialogue = "Father Manstien has always been obsessed with eradicating vampires from existence. He was babbling on about someone named Melissa and a wyrm before he left on his quest."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end