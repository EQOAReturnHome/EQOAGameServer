function event_say()
diagOptions = {}
    npcDialogue = "Keep off of the amphitheater's stage. Only senators, actors, and invited guests are allowed up here."
SendDialogue(mySession, npcDialogue, diagOptions)
end