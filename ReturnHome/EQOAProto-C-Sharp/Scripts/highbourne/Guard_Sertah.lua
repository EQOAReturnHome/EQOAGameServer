function event_say()
diagOptions = {}
    npcDialogue = "This is the worker's quarters. You clearly are not a dock worker. Move along before I revoke your visa and expel you from the city."
SendDialogue(mySession, npcDialogue, diagOptions)
end