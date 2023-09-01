function event_say()
diagOptions = {}
    npcDialogue = "I was once a bard in Tethelin. Though I endeavored to make the most elegant music, it had no potency when engaging in the bardic arts. Though I continue to practice my song, I am much more effective as a guard."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end