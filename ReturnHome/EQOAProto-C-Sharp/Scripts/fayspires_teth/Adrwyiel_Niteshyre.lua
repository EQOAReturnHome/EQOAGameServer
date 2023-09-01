function event_say()
diagOptions = {}
    npcDialogue = "We must protect our ancient tomes. They contain teachings passed down to us from our ancestors who lived in the great Elddar Forrest. There are many casters of the dark magics that seek this ancient knowledge, and could wreak havoc on the world if they were to obtain it."
SendDialogue(mySession, npcDialogue, diagOptions)
end