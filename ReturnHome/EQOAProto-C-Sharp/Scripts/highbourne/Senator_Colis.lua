function event_say()
diagOptions = {}
    npcDialogue = "I brought Jadin here to show him how ineffective his most recent bill, pertaining to visitor visas, is. To my understanding, it was supposed to generate much needed revenue and yet, I see no one."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end