function event_say()
diagOptions = {}
    npcDialogue = "I have been studying Anja's methods and believe I can improve them so the Rose of Renewal would no longer be required…though it would be much more expensive."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end