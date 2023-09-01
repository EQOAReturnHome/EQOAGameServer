function event_say()
diagOptions = {}
    npcDialogue = "Once I anoint someone as a cleric of Tunare, there is no going back to their old life. That time for them is gone, never to return. They are now lifetime servants to the goddess, until she deems it fit you should leave this plane of existence, and return to her."
SendDialogue(mySession, npcDialogue, diagOptions)
end