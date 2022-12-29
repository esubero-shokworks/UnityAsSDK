package com.unity.mynativeapp

import android.Manifest
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.result.contract.ActivityResultContracts
import androidx.fragment.app.Fragment

import com.unity.mynativeapp.databinding.FragmentUIOverlayBinding

class UIOverlayFragment : Fragment() {

    private lateinit var binding: FragmentUIOverlayBinding
    private var mUnity: UnityCommunication? = null

    private var resultPermissions =
        registerForActivityResult(ActivityResultContracts.RequestMultiplePermissions()) { permissions ->
            var isGrantAll = false
            permissions.entries.forEach {
                val permissionName = it.key
                isGrantAll = it.value
            }
            if (isGrantAll) {
//                binding.cvVideo.open()
            } else {
                requireActivity().finish()
            }
        }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentUIOverlayBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        camera()
        btns()
//        requestPermissions()
    }

    private fun camera() =
        binding.apply {
//            cvVideo.setLifecycleOwner(viewLifecycleOwner)
            tvFlip.setOnClickListener {
//                cvVideo.toggleFacing()
            }
            ivClose.setOnClickListener {
                mUnity?.finished()
            }
        }

    private fun btns() =
        binding.apply {
            btnShowMain.setOnClickListener {
                mUnity?.showMain()
            }
            btnSendMsg.setOnClickListener {
                mUnity?.sendMsg()
            }
            btnUnload.setOnClickListener {
                mUnity?.unload()
            }
            btnFinish.setOnClickListener {
                mUnity?.finished()
            }
        }

    private fun requestPermissions() =
        resultPermissions.launch(
            arrayOf(
                Manifest.permission.RECORD_AUDIO,
                Manifest.permission.CAMERA,
                Manifest.permission.READ_EXTERNAL_STORAGE
            )
        )

    fun setUnityPlayer(unity: UnityCommunication) {
        mUnity = unity
    }

}

interface UnityCommunication {
    fun showMain()
    fun sendMsg()
    fun unload()
    fun finished()
}